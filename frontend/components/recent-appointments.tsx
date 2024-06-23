import { Appointment } from "@/types/types";
import { Avatar, AvatarFallback, AvatarImage } from "./ui/avatar";
import { CalendarIcon } from "lucide-react";
import { format } from "date-fns";

interface RecentAppointmentsProps {
  appointments: Appointment[];
}

export function RecentAppointments({ appointments }: RecentAppointmentsProps) {
  return (
    <div className="space-y-8">
      {appointments.map((appointment) => (
        <div key={appointment.id} className="flex items-center">
          <Avatar className="h-9 w-9">
            <AvatarFallback>
              {appointment.doctor.firstName[0]}
              {appointment.doctor.lastName[0]}
            </AvatarFallback>
          </Avatar>
          <div className="ml-4 space-y-1">
            <p className="text-sm font-medium leading-none">
              {appointment.doctor.firstName} {appointment.doctor.lastName}
            </p>
            <p className="text-sm text-muted-foreground">
              {appointment.doctor.email}
            </p>
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
          </div>
        </div>
      ))}
    </div>
  );
}
