import i18n from "i18next";

import { initReactI18next } from "react-i18next";
import daDK from "./da/translations.json";
i18n.use(initReactI18next).init({
  resources: {
    da: daDK,
  },
  fallbackLng: "da",
  debug: true,
  ns: ["translations"],
  defaultNS: "translations",
  interpolation: {
    escapeValue: false,
  },
});

export default i18n;
