import { useEffect, useState } from "react";
import { Box } from "@mui/material";
import PlaylistWidget from "../widgets/playlistWidget";
import { useInView } from "react-intersection-observer";
import Playlist from '../../models/playlist';

const PlaylistsPage = () => {
    const limit = 50;
    const [offset, setOffset] = useState(0);
    const [playlists, setPlaylists] = useState([]);
    const [totalPlaylists, setTotalPlaylists] = useState(0);

    const fetchData = async () => {
        const response = await fetch(`https://localhost:7215/api/playlist?limit=${limit}&offset=${offset}`, 
        {
            credentials: 'include'
        });
        const jsonResponse = await response.json();
        return jsonResponse;
    }

    const loadData = async () => {
        const response = await fetchData();
        if (response && response.data) {
            console.log(response);
            setPlaylists(playlists.concat(response.data));
            setTotalPlaylists(response.total);
            setOffset(offset + limit);
        }
    }

    const { ref } = useInView({
        onChange: (inView) => {
            if (inView && offset < totalPlaylists) {
                loadData();
            }
        }
    });

    useEffect(() => {
        loadData();
    }, []);

    return (
        <>
            {playlists.map((item: Playlist, index, array) => (

                index === array.length - 1 ? (
                    <div key={item.id} ref={ref} >
                        <PlaylistWidget
                            playlistId={item.id}
                            playlistName={item.name} />
                    </div>
                ) : (
                    <div key={item.id}>
                        <PlaylistWidget
                            playlistId={item.id}
                            playlistName={item.name} />
                    </div>
                )
            ))}

        </>
    )
};

export default PlaylistsPage;
