import { useEffect, useState } from "react";
import { Box } from "@mui/material";
import PlaylistWidget from "../widgets/playlistWidget";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../../state/store";
import { fetchPlaylists } from "./playlistSlice";
import { useInView } from "react-intersection-observer";
import Playlist from '../../models/playlist';

const PlaylistsPage = () => {
    const limit = 50;
    const [offset, setOffset] = useState(limit);
    const playlists = useSelector((state: RootState) => state.playlistSlice.playlists);
    const loadData = () => {
        setOffset(offset + limit);
        dispatch(fetchPlaylists({ limit, offset }));
    }

    const dispatch = useDispatch<AppDispatch>();
    const { ref } = useInView({
        onChange: (inView) => {
            if (inView) {
                loadData();
            }
        }
    });

    useEffect(() => {
        loadData();
    }, []);

    return (
        <>
            {playlists.map((o, index, array) => (
                index === array.length - 1 ?
                    (
                        <div key={o.id} ref={ref}>
                            <PlaylistWidget
                                playlistId={o.id}
                                playlistName={o.name} />
                        </div>

                    ) :
                    (
                        <div key={o.id}>
                            <PlaylistWidget
                                playlistId={o.id}
                                playlistName={o.name} />
                        </div>
                    )

            ))}

        </>
    )
};

export default PlaylistsPage;
