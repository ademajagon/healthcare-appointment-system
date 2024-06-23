"use client";

import useSWR from "swr";

const fetcher = (url: string) => fetch(url).then((res) => res.json());

interface Doctor {
  id: string;
  firstName: string;
  lastName: string;
  specialization: string;
  isAvailable: boolean;
  biography: string;
  imageUrl: string;
}

export const useDoctors = () => {
  const { data, error } = useSWR<Doctor[]>(
    `${process.env.NEXT_PUBLIC_API_URL}/api/Doctors`,
    fetcher
  );
  return {
    doctors: data,
    isLoading: !error && !data,
    isError: error,
  };
};
