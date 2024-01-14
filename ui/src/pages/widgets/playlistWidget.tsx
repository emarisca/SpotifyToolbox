import { Box, Button } from "@mui/material";

interface PlaylistType {
    playlistId: string,
    playlistName: string
};

const PlaylistWidget = ({
    playlistId,
    playlistName
}: PlaylistType) => {
    return (
        <Box
            display={ "flex" }
            justifyContent={ "space-between" }
            alignItems={ "center" }>

                <p>{ playlistId }</p>
                <p>{ playlistName }</p>
                <Button variant="contained">Contained</Button>
        </Box>
    );
};

export default PlaylistWidget;