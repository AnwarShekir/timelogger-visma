import { Company, CreateCompany, Project } from "../models";
import { ApiResponse } from "./api.response";
import timeLoggerApi from "./axios.config";

const CONTROLLER = "/company";

const routes = {
  base: () => CONTROLLER,
  single: (id: string) => `${CONTROLLER}/${id}`,
  listProjects: (id: string) => `${CONTROLLER}/${id}/projects`,
};

const CompanyService = () => {
  const create = async (dto: CreateCompany): Promise<void> => {
    await timeLoggerApi.post(routes.base(), dto);
  };

  const single = async (id: string): Promise<Company> => {
    const apiResult = await timeLoggerApi.get<ApiResponse<Company>>(
      routes.single(id)
    );
    return apiResult.data.result;
  };

  const list = async (): Promise<Company[]> => {
    const apiResult = await timeLoggerApi.get<ApiResponse<Company[]>>(
      routes.base()
    );
    return apiResult.data.result;
  };

  const listProjects = async (id: string): Promise<Project[]> => {
    const apiResult = await timeLoggerApi.get<ApiResponse<Project[]>>(
      routes.listProjects(id)
    );
    return apiResult.data.result;
  };

  return {
    create,
    single,
    list,
    listProjects,
  };
};

export default CompanyService;
