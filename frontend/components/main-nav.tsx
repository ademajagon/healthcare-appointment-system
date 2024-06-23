"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { cn } from "@/lib/utils";

export function MainNav({
  className,
  ...props
}: React.HTMLAttributes<HTMLElement>) {
  const pathname = usePathname();

  return (
    <nav
      className={cn("flex items-center space-x-4 lg:space-x-6", className)}
      {...props}
    >
      <Link
        href="/dashboard"
        className={cn(
          "text-sm font-medium transition-colors hover:text-primary",
          {
            "text-muted-foreground": pathname !== "/dashboard",
          }
        )}
      >
        Dashboard
      </Link>
      <Link
        href="/dashboard/doctors"
        className={cn(
          "text-sm font-medium transition-colors hover:text-primary",
          {
            "text-muted-foreground": pathname !== "/dashboard/doctors",
          }
        )}
      >
        Doctors
      </Link>
      <Link
        href="/dashboard/appointments"
        className={cn(
          "text-sm font-medium transition-colors hover:text-primary",
          {
            "text-muted-foreground": pathname !== "/dashboard/appointments",
          }
        )}
      >
        Appointments
      </Link>
    </nav>
  );
}
