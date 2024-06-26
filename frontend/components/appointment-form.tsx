"use client";

import axios from "axios";
import { useForm, SubmitHandler } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import {
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormMessage,
} from "@/components/ui/form";
import { Calendar } from "@/components/ui/calendar";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Calendar as CalendarIcon } from "lucide-react";
import { format } from "date-fns";
import { cn } from "@/lib/utils";
import { useToast } from "@/components/ui/use-toast";
import { useRouter } from "next/navigation";
import { ToastAction } from "@/components/ui/toast";
import { useUserStore } from "@/stores/useUserStore";

const appointmentSchema = z.object({
  appointmentDate: z.string().refine(
    (val) => {
      const date = new Date(val);
      return date > new Date();
    },
    {
      message: "You cannot book appointments in the past.",
    }
  ),
  appointmentTime: z.string().min(1, "Appointment time is required"),
  notes: z.string().max(500, "Notes must not exceed 500 characters").optional(),
});

type AppointmentSchema = z.infer<typeof appointmentSchema>;

interface AppointmentFormProps {
  doctorId: number;
  doctorName: string;
}

export const createAppointment = async (appointment: any) => {
  const response = await axios.post(
    `${process.env.NEXT_PUBLIC_API_URL}/api/Appointments`,
    appointment,
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  return response.data;
};

export default function AppointmentForm({
  doctorId,
  doctorName,
}: AppointmentFormProps) {
  const router = useRouter();
  const { toast } = useToast();
  const { user } = useUserStore();

  const form = useForm<AppointmentSchema>({
    resolver: zodResolver(appointmentSchema),
    defaultValues: {
      appointmentDate: "",
      appointmentTime: "",
      notes: "",
    },
  });

  const onSubmit: SubmitHandler<AppointmentSchema> = async (data) => {
    const appointmentDate = new Date(data.appointmentDate);
    const [hours, minutes] = data.appointmentTime.split(":").map(Number);
    appointmentDate.setHours(hours, minutes);

    const appointment = {
      ...data,
      patientId: user && user.id,
      doctorId,
      appointmentDate: appointmentDate.toISOString(),
      appointmentTime: appointmentDate.toISOString(),
    };

    try {
      const result = await createAppointment(appointment);
      toast({
        title: "Appointment Created",
        description: `Your appointment with Dr. ${doctorName} has been scheduled for ${format(
          appointmentDate,
          "PPP 'at' p"
        )}.`,
        action: <ToastAction altText="Goto schedule to undo">Undo</ToastAction>,
      });
      router.push("/dashboard/appointments");
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to create appointment. Please try again.",
        action: <ToastAction altText="Goto schedule to undo">Undo</ToastAction>,
      });
    }
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
        <FormField
          control={form.control}
          name="appointmentDate"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Appointment Date</FormLabel>
              <FormControl>
                <Popover>
                  <PopoverTrigger asChild>
                    <Button
                      variant={"outline"}
                      className={cn(
                        "w-full justify-start text-left font-normal",
                        !field.value && "text-muted-foreground"
                      )}
                    >
                      <CalendarIcon className="mr-2 h-4 w-4" />
                      {field.value ? (
                        format(new Date(field.value), "PPP")
                      ) : (
                        <span>Pick a date</span>
                      )}
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-auto p-0">
                    <Calendar
                      mode="single"
                      selected={field.value ? new Date(field.value) : undefined}
                      onSelect={(date) =>
                        date && field.onChange(date.toISOString())
                      }
                      initialFocus
                    />
                  </PopoverContent>
                </Popover>
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="appointmentTime"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Appointment Time</FormLabel>
              <FormControl>
                <Input type="time" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="notes"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Notes</FormLabel>
              <FormControl>
                <Textarea {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit" onClick={() => {}}>
          Book Appointment with Dr. {doctorName}
        </Button>
      </form>
    </Form>
  );
}
