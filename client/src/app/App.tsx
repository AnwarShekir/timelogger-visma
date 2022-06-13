import * as React from "react";
import "./tailwind.generated.css";

import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
import Shell from "./views/shell";

export default function App() {
  return (
    <>
      <header className="bg-gray-900 text-white flex items-center h-12 w-full">
        <div className="container mx-auto">
          <a className="navbar-brand" href="/">
            Timelogger
          </a>
        </div>
      </header>

      <main>
        <div className="container mx-auto">
          <Shell />
        </div>
      </main>
    </>
  );
}
