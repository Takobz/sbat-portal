import axios, { AxiosResponse } from 'axios';
import UserLoginRequest from '../Models/SbatApi/UserLoginRequest';
import UserLoginResponse from '../Models/SbatApi/UserLoginResponse';

interface ISbatApiService {
    (loginUser: UserLoginRequest): Promise<UserLoginResponse>
}

const loginUser : ISbatApiService = async (loginRequest: UserLoginRequest) => {
    //validation here
    return axios.post<UserLoginRequest, AxiosResponse<UserLoginResponse>>("url-here")
    .then();
}

export default loginUser