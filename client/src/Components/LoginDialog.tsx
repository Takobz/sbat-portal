import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Grid,
  TextField,
} from "@mui/material";

interface LoginDialogProps {
  open: boolean;
  onClose: () => void;
  onLoginClick: () => void;
}


//fix username bug
const LoginDialog = (props: LoginDialogProps) => {
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
              />
            </Grid>
          </Grid>
          <Grid item xs={12}>
            <Grid container justifyContent="center" alignItems="center">
              <TextField
                id="outline-username"
                label="Passowrd"
                variant="outlined"
              />
            </Grid>
          </Grid>
        </Grid>
      </DialogContent>
      <DialogActions>
        <Button>Login</Button>
      </DialogActions>
    </Dialog>
  );
};

export default LoginDialog;
