"use client";

import { useAuth } from "../context/AuthContext";
import { usePathname } from "next/navigation";
import { useEffect } from "react";
import LoadingSpinner from "@/components/ui/loading";

export const withAuth = (WrappedComponent: React.ComponentType) => {
  return (props: any) => {
    const { isAuthenticated } = useAuth();
    const pathname = usePathname();

    useEffect(() => {
      if (!isAuthenticated && pathname !== "/login") {
        window.location.href = "/login";
      }
    }, [isAuthenticated, pathname]);

    if (!isAuthenticated) {
      return <LoadingSpinner />;
    }

    return <WrappedComponent {...props} />;
  };
};
