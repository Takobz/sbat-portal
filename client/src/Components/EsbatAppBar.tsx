import { AppBar, IconButton, Toolbar, Typography, Button } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu"
import  LoginDialog from "./LoginDialog";
import { useState } from "react";

type AppBarProps = {
    label: string
};

const EsbatAppBar = ({ label }: AppBarProps) => {
  const [open, setOpen] = useState(false);

  const handleOpen = () =>{
    setOpen((prevOpen) =>{
      return !prevOpen
    });
  }

  const handleClose = () =>{
    setOpen(false);
  }
  
  return (
    <>
      <AppBar position="fixed">
        <Toolbar>
            <IconButton edge="start" color="inherit" aria-label="menu" sx={{ mr: 2 }}>
                <MenuIcon />
            </IconButton>
            <Typography variant="h6" noWrap component="div" sx={{ flexGrow: 1 }}>
                {label}
            </Typography>
            <Button color="inherit" onClick={handleOpen}>Login</Button>
        </Toolbar>
      </AppBar>
      <LoginDialog open={open} onClose={handleClose}></LoginDialog>
    </>
  );
};

export default EsbatAppBar;
