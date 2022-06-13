import React, { useEffect } from "react";
import { useState } from "react";
import ProjectService from "../api/projects";
import ProjectTable from "../components/ProjectTable";
import { Project } from "../models";

const ProjectTab = (): JSX.Element => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [selectedProject, setSelectedProject] = useState<Project | undefined>();

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

  return (
    <>
      <label>hello</label>
      <ProjectTable
        projects={projects}
        onProjectSelect={onSelectProject}
      ></ProjectTable>
    </>
  );
};

export default ProjectTab;
