"use client";

import { useDoctors } from "@/hooks/useDoctors";
import { useRouter } from "next/navigation";
import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatar";

export function Doctors() {
  const router = useRouter();

  const { doctors, isLoading, isError } = useDoctors();

  if (isError) return <div>Failed to load</div>;
  if (isLoading) return <div>Loading...</div>;

  const handleRowClick = (id: string) => {
    router.push(`/dashboard/doctors/${id}`);
  };

  return (
    <Table>
      <TableCaption>A list of doctors.</TableCaption>
      <TableHeader>
        <TableRow>
          <TableHead className="w-[100px]">Avatar</TableHead>
          <TableHead>First Name</TableHead>
          <TableHead>Last Name</TableHead>
          <TableHead>Specialization</TableHead>
          <TableHead className="text-right">Is Available</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {doctors?.map((doctor) => (
          <TableRow
            key={doctor.id}
            onClick={() => handleRowClick(doctor.id)}
            className="cursor-pointer"
          >
            <TableCell className="font-medium">
              <Avatar>
                <AvatarImage
                  src={doctor.imageUrl}
                  alt={`${doctor.firstName} ${doctor.lastName}`}
                  className="object-cover"
                />
                <AvatarFallback>
                  {doctor.firstName[0]}
                  {doctor.lastName[0]}
                </AvatarFallback>
              </Avatar>
            </TableCell>
            <TableCell>{doctor.firstName}</TableCell>
            <TableCell>{doctor.lastName}</TableCell>
            <TableCell>{doctor.specialization}</TableCell>
            <TableCell className="text-right">
              {doctor.isAvailable ? "Yes" : "No"}
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
