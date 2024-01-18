import { Button } from "@mui/material";

const LoginPage = () => {

    const handleLoginButtonClick = async () => {
        const response = await fetch('https://localhost:7215/api/auth/getAuthorizationUri');
        if (response && response.ok) {
            window.location.href = await response.text();
        }
    }

    return(
        <Button onClick={handleLoginButtonClick}>Login with Spotify</Button>
    );
}

export default LoginPage;