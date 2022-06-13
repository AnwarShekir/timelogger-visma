import React, { useEffect } from "react";
import { useState } from "react";
import CompanyService from "../api/company";
import CompanyTable from "../components/CompanyTable";
import { Company, CreateCompany } from "../models";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import CreateCompanyModal from "./CreateCompany";

const CompanyTab = (): JSX.Element => {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [selectedCompany, setSelectedCompany] = useState<Company | undefined>();
  const [showCreate, setShowCreate] = useState<boolean>(false);

  const getCompanies = async () => {
    try {
      const result = await CompanyService().list();
      setCompanies(result);
    } catch (error) {
      //todo handle error
    }
  };

  useEffect(() => {
    getCompanies();
  }, []);

  const getCompanyProjects = async (id: string) => {
    try {
      await CompanyService().listProjects(id);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (!selectedCompany) return;
    getCompanyProjects(selectedCompany.id);
  }, [selectedCompany]);

  const onSelectCompany = (company?: Company) => {
    setSelectedCompany(company);
  };

  const onCompanyCreate = async (company: CreateCompany) => {
    await CompanyService().create(company);
    setShowCreate(false);
    await getCompanies();
  };

  return (
    <>
      <Button onClick={() => setShowCreate(true)} label="Opret ny"></Button>
      <CompanyTable
        companies={companies}
        onCompanySelect={onSelectCompany}
      ></CompanyTable>
      <Dialog
        header="Opret ny virksomhed"
        visible={showCreate}
        style={{ width: "30vw" }}
        onHide={() => setShowCreate(false)}
      >
        <CreateCompanyModal
          onCreateCompany={onCompanyCreate}
          onClose={() => setShowCreate(false)}
        />
      </Dialog>
    </>
  );
};

export default CompanyTab;
