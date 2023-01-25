import EsbatAppBar from "./Components/Banners/EsbatAppBar";
import * as AppConstants from './constants/AppConstants'
import "./App.css";

function App() {
  return (
    <>
      <EsbatAppBar label={AppConstants.APP_LABEL} />
    </>
  );
}

export default App;
