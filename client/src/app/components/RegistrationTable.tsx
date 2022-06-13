import React from "react";
import { DataTable } from "primereact/datatable";
import { TimeRegistration } from "../models";
import { Column } from "primereact/column";

interface Props {
  registrations: TimeRegistration[];
  hourlyRate: number;
}
const RegistrationTable = ({ registrations, hourlyRate }: Props) => {
  const calculateTime = (minutes: number) => {
    const min = minutes % 60;
    const hour = Math.floor(minutes / 60);

    return `${hour} Timer og ${min} minutter`;
  };
  const timeColumn = (value: TimeRegistration) => {
    return <label>{calculateTime(value.minutes)}</label>;
  };

  const priceColum = (value: TimeRegistration) => {
    const minuteRate = hourlyRate / 60;
    return <label>{(minuteRate * value.minutes).toFixed(2)} Dkr.</label>;
  };

  const timestampColumn = (value: TimeRegistration) => {
    const date = new Date(value.date);
    return <label>{date.toLocaleDateString()}</label>;
  };

  return (
    <DataTable
      value={registrations}
      paginator
      rows={5}
      dataKey="id"
      filterDisplay="row"
      responsiveLayout="scroll"
      emptyMessage="Ingen Registringer på projektet"
    >
      <Column
        field="date"
        showFilterMenu={false}
        sortable
        body={timestampColumn}
        header="Dato"
      ></Column>
      <Column
        field="minutes"
        showFilterMenu={false}
        sortable
        body={timeColumn}
        header="Tid"
      ></Column>
      <Column header={`Pris (${hourlyRate}/time)`} body={priceColum}></Column>
    </DataTable>
  );
};

export default RegistrationTable;
