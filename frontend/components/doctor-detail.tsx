"use client";

import { useParams } from "next/navigation";
import useSWR from "swr";
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatar";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";
import { Badge } from "./ui/badge";
import AppointmentForm from "./appointment-form";

const fetcher = (url: string) => fetch(url).then((res) => res.json());

export function DoctorDetail() {
  const { id } = useParams();

  const {
    data: doctor,
    error,
    isLoading,
  } = useSWR(`${process.env.NEXT_PUBLIC_API_URL}/api/Doctors/${id}`, fetcher);

  if (error) return <div>Failed to load</div>;
  if (isLoading) return <div>Loading...</div>;

  const additionalInfo = `
    Dr. ${doctor.firstName} ${doctor.lastName} has over 20 years of experience in the medical field. 
    They have been recognized for their outstanding contributions to medical research and patient care. 
    Dr. ${doctor.lastName} is fluent in multiple languages, including English, Spanish, and French. 
    In their free time, they enjoy hiking, reading, and volunteering at local community health clinics.`;

  return (
    <Card className="max-w-2xl mx-auto">
      <CardHeader className="flex items-center">
        <Avatar className="w-32 h-32">
          <AvatarImage
            src={doctor.imageUrl}
            alt={`${doctor.firstName} ${doctor.lastName}`}
            className="object-cover"
          />
          <AvatarFallback className="text-4xl">
            {doctor.firstName[0]}
            {doctor.lastName[0]}
          </AvatarFallback>
        </Avatar>
        <div className="items-center justify-center flex flex-col">
          <CardTitle className="text-2xl font-bold">
            {doctor.firstName} {doctor.lastName}
          </CardTitle>
          <Badge>{doctor.specialization}</Badge>
        </div>
      </CardHeader>
      <CardContent className="space-y-12">
        <div className="space-y-4">
          <div>
            <h2 className="font-semibold">Biography</h2>
            <p className="text-sm">{doctor.biography}</p>
          </div>
          <div>
            <h2 className="font-semibold">Availability</h2>
            <p className="text-sm">{doctor.isAvailable ? "Yes" : "No"}</p>
          </div>
          <div>
            <h2 className="font-semibold">Additional Info</h2>
            <p className="text-sm">{additionalInfo}</p>
          </div>
        </div>

        <div>
          <h2 className="font-semibold">Book an Appointment</h2>
          <AppointmentForm
            doctorId={doctor.id}
            doctorName={`${doctor.firstName} ${doctor.lastName}`}
          />
        </div>
      </CardContent>
    </Card>
  );
}
