import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Grid,
  TextField,
} from "@mui/material";
import { useState } from "react";
import UserLoginRequest from "../../Models/SbatApi/Request/UserLoginRequest";

interface LoginDialogProps {
  open: boolean;
  onClose: () => void;
  onAuth: (loginUser: UserLoginRequest) => void;
}

//fix username bug
const LoginDialog = (props: LoginDialogProps) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const onUsernameChange = (username: string) => {
    setUsername(username);
  }

  const onPasswordChange = (password: string) => {
    setPassword(password);
  }

  const onLoginClick = () => {
    props.onAuth({ username: username, password: password});
  }

  return (
    <Dialog open={props.open} onClose={props.onClose}>
      <DialogTitle>Sign In</DialogTitle>
      <DialogContent>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <Grid container justifyContent="center" alignItems="center">
              <TextField
                id="outline-username"
                label="Username"
                variant="outlined"
                onChange={(e) => onUsernameChange(e.target.value)}
              />
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid container justifyContent="center" alignItems="center">
              <TextField
                id="outline-password"
                label="Passowrd"
                variant="outlined"
                type="password"
                onChange={(e) => onPasswordChange(e.target.value)}
              />
            </Grid>
          </Grid>
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button onClick={onLoginClick}>Login</Button>
      </DialogActions>
    </Dialog>
  );
};

export default LoginDialog;
