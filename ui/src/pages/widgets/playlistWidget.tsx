import { Box, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

interface PlaylistType {
    playlistId: string,
    playlistName: string
};

const PlaylistWidget = ({
    playlistId,
    playlistName
}: PlaylistType) => {
    const navigate = useNavigate();
    const handleClick = () => {
        navigate(`/duplicates?playlistId=${playlistId}&playlistName=${playlistName}`);
    }

    return (
        <Box
            display="flex" alignItems="center" justifyContent="space-between">

            <p>{playlistId}</p>
            <p>{playlistName}</p>
            <Button variant="contained" onClick={handleClick}>Contained</Button>
        </Box>
    );
};

export default PlaylistWidget;