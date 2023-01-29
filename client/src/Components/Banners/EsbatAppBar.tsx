import {
  AppBar,
  IconButton,
  Toolbar,
  Typography,
  Button,
  Container,
  Box,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import LoginDialog from "../Login/LoginDialog";
import { useState } from "react";
import UserLoginResponse from "../../Models/SbatApi/Response/UserLoginResponse";
import UserLoginRequest from "../../Models/SbatApi/Request/UserLoginRequest";
import LoginUser from "../../Services/SbatApiService";
import { Outlet } from "react-router-dom";

type AppBarProps = {
  label: string;
};

const EsbatAppBar = ({ label }: AppBarProps) => {
  const [open, setOpen] = useState(false);
  const [isUserAuthd, setIsUserAuthd] = useState(false);
  const [loggedInUser, setLoggedInUser] = useState<UserLoginResponse>(
    {} as UserLoginResponse
  );

  const handleOpen = () => {
    setOpen((prevOpen) => {
      return !prevOpen;
    });
  };

  const handleClose = () => {
    setOpen(false);
  };

  const getUserLoginData = async (loginUser: UserLoginRequest) => {
    try {
      const result = await LoginUser({
        username: loginUser.username,
        password: loginUser.password,
      });
      setLoggedInUser(result); //TODO: route to appropriarte page with data
    } catch (e) {
      //inform user in some way we have an err
      console.log(e);
    }
  };

  const handleAuth = (loginUser: UserLoginRequest) => {
    console.log(loginUser);
    getUserLoginData(loginUser);

    setIsUserAuthd((prevAuthd) => {
      setOpen(false);
      return !prevAuthd;
    });
  };

  return (
    <>
      <AppBar position="fixed">
        <Toolbar>
          <IconButton
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" noWrap component="div" sx={{ flexGrow: 1 }}>
            {label}
          </Typography>
          {isUserAuthd ? (
            <IconButton>
              <AccountCircleIcon />
            </IconButton>
          ) : (
            <Button color="inherit" onClick={handleOpen}>
              Login
            </Button>
          )}
        </Toolbar>
      </AppBar>
      <Toolbar/>
      <Outlet />
      <LoginDialog
        open={open}
        onClose={handleClose}
        onAuth={handleAuth}
      ></LoginDialog>
    </>
  );
};

export default EsbatAppBar;
