import { useRouteError } from "react-router-dom";

const ErrorPrompt = () => {
  const error = useRouteError() as any;
  console.log(error);

  return (
    <>
      <h1>Ooops!</h1>
      <p>Sorry, an unexpected error has occurred</p>
      <p>
        <i>{(error.message && error.statuText) ? `${error.status} : ${error.statusText}`
            : `The Devs messed up something! Fixing it`}</i><i>🔨</i>
      </p>
    </>
  );
};

export default ErrorPrompt;
