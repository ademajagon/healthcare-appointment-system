import { Appointments } from "@/components/appointments";
import { CalendarDateRangePicker } from "@/components/date-range-picker";
import { DoctorAvailability } from "@/components/doctor-availability";
import { Doctors } from "@/components/doctors";
import { MainNav } from "@/components/main-nav";
import ProtectedRoute from "@/components/protected-route";
import { Search } from "@/components/search";
import { Button } from "@/components/ui/button";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { UserNav } from "@/components/user-nav";
import { Metadata } from "next";

export const metadata: Metadata = {
  title: "Dashboard",
  description: "Example dashboard app built using the components.",
};

export default function AppointmentPage() {
  return (
    <>
      <div className="flex items-center justify-between space-y-2  sm:flex-row ">
        <h2 className="text-3xl font-bold tracking-tight">Appointments</h2>
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
          <Appointments />
        </TabsContent>
      </Tabs>
    </>
  );
}
