import axios, { AxiosRequestConfig } from "axios";
import { ApiResponse } from "./api.response";

const BASE_URL = "http://localhost:3001/api";

const standardConfig: AxiosRequestConfig = {
  baseURL: BASE_URL,
  timeout: 5000,
};
const timeLoggerApi = axios.create({
  ...standardConfig,
});

timeLoggerApi.interceptors.request.use(
  (request) => {
    //no auth in this app, otherwise set token.
    return request;
  },
  (error) => {
    return Promise.reject(error);
  }
);

timeLoggerApi.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    const errorResult = error.response.data as ApiResponse<unknown>;
    return Promise.reject(errorResult.errorMessage);
  }
);

export default timeLoggerApi;
