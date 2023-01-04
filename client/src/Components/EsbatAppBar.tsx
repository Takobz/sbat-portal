import { AppBar, IconButton, Toolbar, Typography, Button } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu"

type AppBarProps = {
    label: string
};

const EsbatAppBar = ({ label }: AppBarProps) => {
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
            <Button color="inherit">Login</Button>
        </Toolbar>
      </AppBar>
    </>
  );
};

export default EsbatAppBar;
