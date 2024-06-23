import { Toaster } from "@/components/ui/toaster";

interface AuthLayoutProps {
  children: React.ReactNode;
}

export default function AuthLayout({ children }: AuthLayoutProps) {
  return (
    <div className="min-h-screen">
      {children}
      <Toaster />
    </div>
  );
}
