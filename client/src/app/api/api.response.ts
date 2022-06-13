export interface ApiResponse<T> {
  result: T;
  errorMessage?: string;
  timeGenerated: Date;
  apiVersion: string;
}
