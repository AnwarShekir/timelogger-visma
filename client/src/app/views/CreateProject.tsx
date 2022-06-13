import React from "react";
import { useState } from "react";
import { Company, CreateProject } from "../models";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import CompanyLookup from "../components/CompanyLookup";
import { Calendar } from "primereact/calendar";
import { InputNumber } from "primereact/inputnumber";

interface Props {
  onCreateProject: (project: CreateProject) => void;
  onClose: () => void;
}

const CreateProjectModal = ({ onClose, onCreateProject }: Props) => {
  const [name, setName] = useState<string>("");
  const [company, setCompany] = useState<Company | undefined>();
  const [deadline, setDeadline] = useState<Date>(new Date());
  const [start, setStart] = useState<Date>(new Date());
  const [hourlyRate, setHourlyRate] = useState<number | null>();

  //could use a libary for forms, like formrik, but keeping it simple.
  const hasErrors = () => {
    if (!name) return true;
    if (!company) return true;
    if (deadline < start) return true;
    if (!hourlyRate) return true;
    if (hourlyRate < 1) return true;
    return false;
  };

  const onCreate = () => {
    if (hasErrors()) return;
    const project: CreateProject = {
      name: name,
      companyId: company!.id,
      deadline: deadline,
      start: start,
      hourlyRate: hourlyRate!,
    };
    onCreateProject(project);
  };

  const onCompanySelect = (company: Company) => {
    setCompany(company);
  };

  //tailwind not working with the component libary, so using normal styles.
  return (
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label>
        Navn
        <InputText
          value={name}
          onChange={(e) => setName(e.target.value)}
        ></InputText>
      </label>
      <label>
        Virksomhed
        <CompanyLookup onSelect={onCompanySelect}></CompanyLookup>
      </label>
      <label>
        Start
        <Calendar
          selectionMode="single"
          value={start}
          dateFormat="dd/mm/yy"
          onChange={(e) => setStart(e.value as Date)}
        ></Calendar>
      </label>
      <label>
        Deadline
        <Calendar
          value={deadline}
          dateFormat="dd/mm/yy"
          selectionMode="single"
          onChange={(e) => setDeadline(e.value as Date)}
        ></Calendar>
      </label>
      <label>
        Timepris
        <InputNumber
          value={hourlyRate}
          onValueChange={(e) => setHourlyRate(e.value)}
          mode="currency"
          currency="DKK"
          locale="da-DK"
        />
      </label>
      <Button onClick={onCreate} label="Opret"></Button>
      <Button onClick={onClose} label="Annuler"></Button>
    </div>
  );
};

export default CreateProjectModal;
