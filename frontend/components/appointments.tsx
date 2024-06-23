"use client";

import axios from "axios";
import useSWR from "swr";
import { format } from "date-fns";
import {
  Card,
  CardHeader,
  CardTitle,
  CardContent,
  CardDescription,
} from "@/components/ui/card";
import { TrashIcon, CalendarIcon } from "@radix-ui/react-icons";
import { Button } from "./ui/button";
import { useUserStore } from "@/stores/useUserStore";
import { useAuth } from "@/context/AuthContext";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";

const fetcher = (url: string, token: string) =>
  axios
    .get(url, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((res) => res.data);

export function Appointments() {
  const user = useUserStore((state) => state.user);
  const { token } = useAuth();

  const { data, error, mutate } = useSWR(
    user && token
      ? [
          `${process.env.NEXT_PUBLIC_API_URL}/api/Appointments/patient/${user.id}`,
          token,
        ]
      : null,
    ([url, token]) => fetcher(url, token)
  );

  if (!user) {
    return <div>Loading...</div>;
  }

  if (error) return <div>Failed to load appointments</div>;
  if (!data) return <div>Loading...</div>;

  const handleUnbook = async (id: number) => {
    try {
      await axios.delete(
        `${process.env.NEXT_PUBLIC_API_URL}/api/Appointments/${id}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      mutate();
    } catch (error) {}
  };

  return (
    <div className="space-y-4">
      {data.map((appointment: any) => (
        <Card key={appointment.id}>
          <CardHeader className="grid grid-cols-[1fr_110px] items-start gap-4 space-y-0">
            <div className="space-y-1">
              <CardTitle>
                Appointment with Dr. {appointment.doctor.firstName}{" "}
                {appointment.doctor.lastName}
              </CardTitle>
              <CardDescription>{appointment.notes}</CardDescription>
            </div>
            <div className="flex items-center space-x-1 rounded-md bg-secondary text-secondary-foreground">
              <AlertDialog>
                <AlertDialogTrigger asChild>
                  <Button variant="secondary" className="shadow-none">
                    <TrashIcon className="mr-2 h-4 w-4" />
                    Unbook
                  </Button>
                </AlertDialogTrigger>
                <AlertDialogContent>
                  <AlertDialogHeader>
                    <AlertDialogTitle>
                      Are you absolutely sure?
                    </AlertDialogTitle>
                    <AlertDialogDescription>
                      This action cannot be undone. This will permanently delete
                      your account and remove your data from our servers.
                    </AlertDialogDescription>
                  </AlertDialogHeader>
                  <AlertDialogFooter>
                    <AlertDialogCancel>Cancel</AlertDialogCancel>
                    <AlertDialogAction
                      onClick={() => handleUnbook(appointment.id)}
                    >
                      Continue
                    </AlertDialogAction>
                  </AlertDialogFooter>
                </AlertDialogContent>
              </AlertDialog>
            </div>
          </CardHeader>
          <CardContent>
            <div className="flex space-x-4 text-sm text-muted-foreground">
              <div className="flex items-center">
                <CalendarIcon className="mr-1 h-4 w-4 fill-sky-400 text-sky-400" />
                {format(new Date(appointment.appointmentDate), "PPP")}
              </div>
              <div>/</div>
              <div className="flex items-center">
                {format(new Date(appointment.appointmentTime), "p")}
              </div>
            </div>
          </CardContent>
        </Card>
      ))}
    </div>
  );
}
