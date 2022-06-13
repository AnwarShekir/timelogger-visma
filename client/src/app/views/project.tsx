import React, { useEffect } from "react";
import { useState } from "react";
import ProjectService from "../api/projects";
import ProjectTable from "../components/ProjectTable";
import { CreateProject, Project } from "../models";
import { Dialog } from "primereact/dialog";
import { Button } from "primereact/button";
import CreateProjectModal from "./CreateProject";
import { Sidebar } from "primereact/sidebar";
import ProjectDetails from "./ProjectDetails";

const ProjectTab = (): JSX.Element => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [selectedProject, setSelectedProject] = useState<Project | undefined>();
  const [showCreate, setShowCreate] = useState<boolean>(false);

  const getProjects = async () => {
    try {
      const result = await ProjectService().list();
      setProjects(result);
    } catch (error) {
      //todo handle error
    }
  };

  useEffect(() => {
    getProjects();
  }, []);

  const getProjectTimeRegistrations = async (id: string) => {
    try {
      await ProjectService().listRegistrations(id);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (!selectedProject) return;
    getProjectTimeRegistrations(selectedProject.id);
  }, [selectedProject]);

  const onSelectProject = (project?: Project) => {
    setSelectedProject(project);
  };

  const onProjectCreate = async (project: CreateProject) => {
    await ProjectService().create(project);
    setShowCreate(false);
    await getProjects();
  };

  return (
    <>
      <Button onClick={() => setShowCreate(true)} label="Opret ny"></Button>
      <ProjectTable
        projects={projects}
        onProjectSelect={onSelectProject}
      ></ProjectTable>
      <Dialog
        header="Opret nyt Project"
        visible={showCreate}
        style={{ width: "30vw" }}
        onHide={() => setShowCreate(false)}
      >
        <CreateProjectModal
          onCreateProject={onProjectCreate}
          onClose={() => setShowCreate(false)}
        />
      </Dialog>
      <Sidebar
        visible={selectedProject !== undefined}
        position="right"
        className="p-sidebar-lg"
        onHide={() => setSelectedProject(undefined)}
      >
        {selectedProject && (
          <ProjectDetails project={selectedProject}></ProjectDetails>
        )}
      </Sidebar>
    </>
  );
};

export default ProjectTab;
