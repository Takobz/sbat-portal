import { createBrowserRouter, Router } from "react-router-dom";
import App from "../App";
import ErrorPrompt from "../Components/Error/ErrorPrompt";
import UserOverviewPage from "../Components/Pages/UserOverviewPage";

const createAppRouters = ()  => {
   return createBrowserRouter([
    {
      path: "/",
      element: <App/>,
      errorElement: <ErrorPrompt />,
      children: [
        {
          path: '/users/:userId',
          element: <UserOverviewPage/>,
          errorElement: <ErrorPrompt />
        }
      ]
    }
  ]);
};

export default createAppRouters;
