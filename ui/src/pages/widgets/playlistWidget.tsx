import { Box } from "@mui/material";

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

                { playlistId }
                { playlistName }

        </Box>
    );
};

export default PlaylistWidget;