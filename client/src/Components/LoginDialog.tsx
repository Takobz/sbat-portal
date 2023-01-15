import { Dialog, DialogTitle } from "@mui/material";

interface LoginDialogProps {
  open: boolean;
  onClose: () => void;
}

const LoginDialog = (props: LoginDialogProps) => {
  return (
    <Dialog open={props.open} onClose={props.onClose}>
      <DialogTitle>Just A Dialog</DialogTitle>
    </Dialog>
  );
};

export default LoginDialog;
