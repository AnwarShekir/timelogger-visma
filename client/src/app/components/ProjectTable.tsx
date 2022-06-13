import React from "react";
import { DataTable } from "primereact/datatable";
import { Project } from "../models";
import { Column } from "primereact/column";

interface Props {
  projects: Project[];
  onProjectSelect: (project?: Project) => void;
}
const ProjectTable = ({ projects, onProjectSelect }: Props) => {
  //using a libary for sorting, but here is the method if needed to do it myself.
  //ofc i would not sort on the state itself, but create a temp list, sort it, then setState.
  // const SortProjects = () => {
  //     projects.sort((a,b) => {
  //         return  b.deadline.getTime() - a.deadline.getTime()
  //     })
  // }

  const deadlineColumn = (value: Project) => {
    const date = new Date(value.deadline);
    return <label>{date.toLocaleDateString()}</label>;
  };

  const startColumn = (value: Project) => {
    const date = new Date(value.start);
    return <label>{date.toLocaleDateString()}</label>;
  };

  return (
    <DataTable
      value={projects}
      paginator
      rows={15}
      dataKey="id"
      filterDisplay="row"
      responsiveLayout="scroll"
      emptyMessage="Ingen projekter oprettet"
      selectionMode="single"
      onSelectionChange={(e) => onProjectSelect(e.value)}
    >
      <Column
        field="name"
        showFilterMenu={false}
        sortable
        header="Navn"
      ></Column>
      <Column
        field="start"
        showFilterMenu={false}
        sortable
        body={startColumn}
        header="Project start"
      ></Column>
      <Column
        field="deadline"
        showFilterMenu={false}
        sortable
        body={deadlineColumn}
        header="Deadline"
      ></Column>
      <Column
        field="hourlyRate"
        showFilterMenu={false}
        sortable
        header="Time pris"
      ></Column>
    </DataTable>
  );
};

export default ProjectTable;
