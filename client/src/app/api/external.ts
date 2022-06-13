import axios from "axios";

const DAWA_URL = "https://api.dataforsyningen.dk/autocomplete";

const dawaApi = axios.create({
  baseURL: DAWA_URL,
});

const routes = {
  query: (q: string) => `?q=${q}&caretpos=18&fuzzy=`,
};

export class AutoCompleteExternalAPIAddress {
  constructor(
    public stormodtagerpostnr: boolean | null = null,
    public type: string = "",
    public tekst: string = "",
    public forslagstekst: string = "",
    public caretpos: number | null = null,
    public data: ExternalAPIAddress | null = null
  ) {}
}

export class ExternalAPIAddress {
  constructor(
    public adresseringsvejnavn: string = "",
    public husnr: string = "",
    public etage: string = "",
    public dør: string = "",
    public postnr: string = "",
    public postnrnavn: string = "",
    public kommunekode: string = "",
    public vejnavn: string = "",
    public vejkode: string = "",
    public adgangsadresseid: string = "",
    public id: string = ""
  ) {}
}

const AddressLookupService = () => {
  const find = async (
    query: string
  ): Promise<AutoCompleteExternalAPIAddress[]> => {
    const result = await dawaApi.get<AutoCompleteExternalAPIAddress[]>(
      routes.query(query)
    );
    return result.data;
  };

  return {
    find,
  };
};

export default AddressLookupService;
