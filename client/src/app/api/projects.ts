import { CreateProject, Project, TimeRegistration } from "../models";
import { ApiResponse } from "./api.response";
import timeLoggerApi from "./axios.config";

const CONTROLLER = "/projects";
const routes = {
  base: () => CONTROLLER,
  single: (id: string) => `${CONTROLLER}/${id}`,
  listRegistrations: (id: string) => `${CONTROLLER}/${id}/timeregistrations`,
};

const ProjectService = () => {
  const create = async (dto: CreateProject): Promise<void> => {
    await timeLoggerApi.post<ApiResponse<void>>(routes.base(), dto);
  };

  const single = async (id: string): Promise<Project> => {
    const apiResult = await timeLoggerApi.get<ApiResponse<Project>>(
      routes.single(id)
    );
    return apiResult.data.result;
  };

  const list = async (): Promise<Project[]> => {
    const apiResult = await timeLoggerApi.get<ApiResponse<Project[]>>(
      routes.base()
    );
    return apiResult.data.result;
  };

  const listRegistrations = async (id: string): Promise<TimeRegistration[]> => {
    const apiResult = await timeLoggerApi.get<ApiResponse<TimeRegistration[]>>(
      routes.listRegistrations(id)
    );
    return apiResult.data.result;
  };

  return {
    create,
    single,
    list,
    listRegistrations,
  };
};

export default ProjectService;
