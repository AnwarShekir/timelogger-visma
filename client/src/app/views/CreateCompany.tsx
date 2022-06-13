import React from "react";
import { useState } from "react";
import { CreateCompany } from "../models";
import { InputText } from "primereact/inputtext";
import AddressLookup from "../components/AddressLookup";
import { Button } from "primereact/button";

interface Props {
  onCreateCompany: (company: CreateCompany) => void;
  onClose: () => void;
}

const CreateCompanyModal = ({ onClose, onCreateCompany }: Props) => {
  const [name, setName] = useState<string>("");
  const [address, setAddress] = useState<string>("");

  //could use a libary for forms, like formrik, but keeping it simple.
  const hasErrors = () => {
    if (!name) return true;
    if (!address) return true;
    return false;
  };

  const onCreate = () => {
    if (hasErrors()) return;
    const company: CreateCompany = {
      name: name,
      address: address,
    };
    onCreateCompany(company);
  };

  const onAddressSelect = (address: string) => {
    setAddress(address);
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
        Addresse
        <AddressLookup onSelect={onAddressSelect}></AddressLookup>
      </label>
      <Button onClick={onCreate} label="Opret"></Button>
      <Button onClick={onClose} label="Annuler"></Button>
    </div>
  );
};

export default CreateCompanyModal;
