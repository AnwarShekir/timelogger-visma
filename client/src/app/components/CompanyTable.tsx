import React from "react";
import { DataTable } from "primereact/datatable";
import { Company } from "../models";
import { Column } from "primereact/column";

interface Props {
  companies: Company[];
  onCompanySelect: (company?: Company) => void;
}
const CompanyTable = ({ companies, onCompanySelect }: Props) => {
  return (
    <DataTable
      value={companies}
      paginator
      rows={15}
      dataKey="id"
      filterDisplay="row"
      responsiveLayout="scroll"
      emptyMessage="Ingen virksomheder oprettet"
      selectionMode="single"
      onSelectionChange={(e) => onCompanySelect(e.value)}
    >
      <Column
        field="name"
        showFilterMenu={false}
        sortable
        header="Navn"
      ></Column>
      <Column
        field="address"
        showFilterMenu={false}
        sortable
        header="Addresse"
      ></Column>
    </DataTable>
  );
};

export default CompanyTable;
