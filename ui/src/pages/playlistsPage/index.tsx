import { useEffect } from "react";
import { Box } from "@mui/material";
import PlaylistWidget from "../widgets/playlistWidget";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../state/store";
import { fetchPlaylists } from "./playlistSlice";

const PlaylistsPage = () => {
    const playlists = useSelector((state: RootState) => state.playlistSlice.playlists);
    const dispatch = useDispatch<AppDispatch>();


    useEffect(() => {
        dispatch(fetchPlaylists());
    }, []);

    return (
        <>
            home page!
            <Box display="flex" alignItems="center">
                {playlists.map(o => (
                    <PlaylistWidget
                        playlistId={o.playlistId}
                        playlistName={o.playlistName} />
                ))}

            </Box>
        </>
    )
};

export default PlaylistsPage;
