import { CreateTimeRegistration, TimeRegistration } from "../models";
import { ApiResponse } from "./api.response";
import timeLoggerApi from "./axios.config";

const CONTROLLER = "/timeregistration";

const routes = {
  base: () => CONTROLLER,
  single: (id: string) => `${CONTROLLER}/${id}`,
};

const TimeRegistrationService = () => {
  const create = async (dto: CreateTimeRegistration): Promise<void> => {
    await timeLoggerApi.post<ApiResponse<void>>(routes.base(), dto);
  };

  const single = async (id: string): Promise<TimeRegistration> => {
    const apiResult = await timeLoggerApi.get<ApiResponse<TimeRegistration>>(
      routes.single(id)
    );
    return apiResult.data.result;
  };

  const list = async (): Promise<TimeRegistration[]> => {
    const apiResult = await timeLoggerApi.get<ApiResponse<TimeRegistration[]>>(
      routes.base()
    );
    return apiResult.data.result;
  };

  return {
    create,
    single,
    list,
  };
};

export default TimeRegistrationService;
