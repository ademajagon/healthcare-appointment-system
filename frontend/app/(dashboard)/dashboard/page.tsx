import { Metadata } from "next";
import { Button } from "@/components/ui/button";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { CalendarDateRangePicker } from "@/components/date-range-picker";
import { Doctors } from "@/components/doctors";
import { DoctorAvailability } from "@/components/doctor-availability";
import { AppointmentsData } from "@/components/appointments-data";

export const metadata: Metadata = {
  title: "Dashboard - Healthcare",
};

export default function DashboardPage() {
  return (
    <>
      <div className="flex items-center justify-between space-y-2  sm:flex-row ">
        <h2 className="text-3xl font-bold tracking-tight">Dashboard</h2>
        <div className="flex items-center space-x-2">
          <div className="hidden sm:block">
            <CalendarDateRangePicker className="" />
          </div>
          <Button disabled>Add Appointment</Button>
        </div>
      </div>
      <Tabs defaultValue="overview" className="space-y-4">
        <TabsList>
          <TabsTrigger value="overview">Doctors</TabsTrigger>
          <TabsTrigger value="analytics" disabled>
            Analytics
          </TabsTrigger>
          <TabsTrigger value="reports" disabled>
            Reports
          </TabsTrigger>
          <TabsTrigger value="notifications" disabled>
            Notifications
          </TabsTrigger>
        </TabsList>
        <TabsContent value="overview" className="space-y-4">
          <DoctorAvailability />

          <AppointmentsData />

          <Doctors />
        </TabsContent>
      </Tabs>
    </>
  );
}
