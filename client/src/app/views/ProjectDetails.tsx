import { Button } from "primereact/button";
import { Calendar } from "primereact/calendar";
import { InputNumber } from "primereact/inputnumber";
import React, { useEffect } from "react";
import { useState } from "react";
import ProjectService from "../api/projects";
import TimeRegistrationService from "../api/timeregistration";
import RegistrationTable from "../components/RegistrationTable";
import { CreateTimeRegistration, Project, TimeRegistration } from "../models";

interface Props {
  project: Project;
}

const ProjectDetails = ({ project }: Props) => {
  const [registrations, setRegistrations] = useState<TimeRegistration[]>([]);
  const [timestamp, setTimestamp] = useState<Date>(new Date());
  const [minutes, setMinutes] = useState<number | null>();

  const getRegistrations = async () => {
    const result = await ProjectService().listRegistrations(project.id);
    setRegistrations(result);
  };

  const registrationHasErrors = () => {
    if (!timestamp) return true;
    if (timestamp > project.deadline) return true;
    if (!minutes) return true;
    if (minutes < 30) return true;
    return false;
  };

  const onCreateRegistration = async () => {
    if (registrationHasErrors()) return;
    const registration: CreateTimeRegistration = {
      date: timestamp!,
      minutes: minutes!,
      projectId: project.id,
    };
    await TimeRegistrationService().create(registration);
    await getRegistrations();
  };

  useEffect(() => {
    getRegistrations();
  }, []);

  return (
    <>
      <div className="flex flex-col w-full">
        <label>
          Dato
          <Calendar
            value={timestamp}
            dateFormat="dd/mm/yy"
            selectionMode="single"
            onChange={(e) => setTimestamp(e.value as Date)}
          ></Calendar>
        </label>
        <label>
          Tid (Minutter)
          <InputNumber
            value={minutes}
            onValueChange={(e) => setMinutes(e.value)}
          ></InputNumber>
        </label>
        <Button label="Opret" onClick={onCreateRegistration}></Button>
      </div>
      <RegistrationTable
        registrations={registrations}
        hourlyRate={project.hourlyRate}
      ></RegistrationTable>
    </>
  );
};

export default ProjectDetails;
