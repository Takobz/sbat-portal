import axios, { AxiosResponse } from "axios";
import UserLoginRequest from "../Models/SbatApi/Request/UserLoginRequest";
import UserLoginResponse from "../Models/SbatApi/Response/UserLoginResponse";
import Response from "../Models/SbatApi/Response/Response";
import { SBAT_API_BASE_URI } from "../constants/AppConstants";

interface ISbatApiService {
  (loginUser: UserLoginRequest): Promise<UserLoginResponse>;
}

const loginUser: ISbatApiService = async (loginRequest: UserLoginRequest) => {
  //validation here
  var sbatApi = axios.create({
    baseURL: SBAT_API_BASE_URI,
  });

  return await sbatApi
    .post<UserLoginRequest, AxiosResponse<Response<UserLoginResponse>>>(
      "login/sign-in",
      loginRequest,
      {
        headers: {
          "content-type": "application/json",
        },
      }
    )
    .then((response) => {
      var apiResponse = response.data;
      if (response.status === 200 && !apiResponse.errors)
        return apiResponse.data;

      throw new Error(apiResponse.errors.join(";"));
    });
};

export default loginUser;
