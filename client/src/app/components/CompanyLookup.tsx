import { AutoComplete } from "primereact/autocomplete";
import React from "react";
import { useState } from "react";
import CompanyService from "../api/company";
import { Company } from "../models";

interface Props {
  onSelect: (company: Company) => void;
}

const CompanyLookup = ({ onSelect }: Props) => {
  const [suggestions, setSuggestions] = useState<Company[]>([]);
  const [query, setQuery] = useState<string>("");

  const searchCompany = async (query: string) => {
    const result = await CompanyService().find(query);
    setSuggestions(result);
  };

  const onSelectValue = (data: Company) => {
    onSelect(data);
  };

  return (
    <AutoComplete
      field="name"
      onSelect={(e) => onSelectValue(e.value)}
      value={query}
      onChange={(e) => setQuery(e.value)}
      completeMethod={(e) => searchCompany(e.query)}
      suggestions={suggestions}
    ></AutoComplete>
  );
};

export default CompanyLookup;
