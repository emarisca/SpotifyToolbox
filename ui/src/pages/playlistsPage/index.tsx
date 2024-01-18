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
            {playlists.map(o => (
                <PlaylistWidget
                    key={o.id}
                    playlistId={o.id}
                    playlistName={o.name} />
            ))}

        </>
    )
};

export default PlaylistsPage;
