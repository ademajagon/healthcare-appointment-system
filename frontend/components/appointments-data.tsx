"use client";

import useSWR from "swr";
import { Overview } from "./overview";
import { RecentAppointments } from "./recent-appointments";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "./ui/card";
import { Button } from "./ui/button";
import { useUserStore } from "@/stores/useUserStore";
import { useAuth } from "@/context/AuthContext";
import axios from "axios";
import { Appointment } from "@/types/types";

const fetcher = (url: string, token: string) =>
  axios
    .get(url, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((res) => res.data);

export function AppointmentsData() {
  const user = useUserStore((state) => state.user);
  const { token } = useAuth();

  const { data, error, mutate } = useSWR<Appointment[]>(
    user && token
      ? [
          `${process.env.NEXT_PUBLIC_API_URL}/api/Appointments/patient/${user.id}`,
          token,
        ]
      : null,
    ([url, token]) => fetcher(url, token as string)
  );

  console.log(data, "DATA");

  return (
    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-7">
      <Card className="col-span-4">
        <CardHeader>
          <CardTitle>Appointments History</CardTitle>
        </CardHeader>
        <CardContent className="pl-2">
          <Overview />
        </CardContent>
      </Card>
      <Card className="col-span-3">
        <CardHeader>
          <CardTitle>Recent Appointments</CardTitle>
          <CardDescription>You have appointments this month.</CardDescription>
        </CardHeader>
        <CardContent>
          <RecentAppointments appointments={data || []} />
        </CardContent>
      </Card>
    </div>
  );
}
