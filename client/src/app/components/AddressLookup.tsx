import { AutoComplete } from "primereact/autocomplete";
import React from "react";
import { useState } from "react";
import AddressLookupService, {
  AutoCompleteExternalAPIAddress,
} from "../api/external";

interface Props {
  onSelect: (address: string) => void;
}

const AddressLookup = ({ onSelect }: Props) => {
  const [suggestions, setSuggestions] = useState<
    AutoCompleteExternalAPIAddress[]
  >([]);
  const [query, setQuery] = useState<string>("");

  const searchAddress = async (query: string) => {
    const result = await AddressLookupService().find(query);
    setSuggestions(result);
  };

  const onSelectValue = (data: AutoCompleteExternalAPIAddress) => {
    onSelect(data.forslagstekst);
  };

  return (
    <AutoComplete
      field="forslagstekst"
      onSelect={(e) => onSelectValue(e.value)}
      value={query}
      onChange={(e) => setQuery(e.value)}
      completeMethod={(e) => searchAddress(e.query)}
      suggestions={suggestions}
    ></AutoComplete>
  );
};

export default AddressLookup;
