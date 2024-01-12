import { useEffect } from "react";
import { Box } from "@mui/material";
import PlaylistWidget from "../widgets/playlistWidget";

const HomePage = () => {

    const fetchPlaylists = async() => {
        const response = await fetch('https://localhost:7215/api/playlist')
        const playlists = await response.json();
        return playlists;
    };

    useEffect(() => {
        fetchPlaylists();
    }, []);

    return (
        <>
            home page!
            <Box display="flex" alignItems="center">
                aa
                {/* <PlaylistWidget 
                    playlistId="asdfsadf"
                    playlistName="asdf"/> */}
            </Box>
        </>
    )
};

export default HomePage;
