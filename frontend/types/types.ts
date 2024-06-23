export interface Doctor {
  id: number;
  firstName: string;
  lastName: string;
  specialization: string;
  biography: string;
  email: string;
}

export interface Patient {
  id: number;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  gender: string;
  email: string;
}

export interface Appointment {
  id: number;
  appointmentDate: string;
  appointmentTime: string;
  doctorId: number;
  doctor: Doctor;
  patientId: number;
  patient: Patient;
  notes: string;
}
