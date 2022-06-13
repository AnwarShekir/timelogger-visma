import { useTranslation } from "react-i18next";
import { Card } from "primereact/card";
import { TabMenu } from "primereact/tabmenu";
import React, { useState } from "react";
import CompanyTab from "./company";
import ProjectTab from "./project";

interface Tab {
  label: string;
  index: number;
  component: JSX.Element;
}

const Shell = (): JSX.Element => {
  const { t } = useTranslation();

  const tabs: Tab[] = [
    {
      label: t("company"),
      component: <CompanyTab />,
      index: 0,
    },
    {
      label: t("projects"),
      component: <ProjectTab />,
      index: 1,
    },
  ];

  const [currentTab, setCurrentTab] = useState<Tab>(tabs[0]);

  const cardHeader = (
    <TabMenu
      model={tabs}
      activeIndex={currentTab.index}
      onTabChange={(e) => setCurrentTab(tabs[e.index])}
    />
  );

  return <Card header={cardHeader}>{currentTab.component}</Card>;
};

export default Shell;
