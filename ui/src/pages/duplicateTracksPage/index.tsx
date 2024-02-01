import { useSearchParams } from "react-router-dom";
import DuplicateTrack from "../../models/duplicateTrack";
import TrackGroupWidget from "../widgets/trackGroupWidget";
import { useEffect, useState } from "react";

const DuplicateTracksPage = () => {
    const [searchParams] = useSearchParams();
    const playlistId = searchParams.get('playlistId');
    const playlistName = searchParams.get('playlistName');
    const [tracks, setTracks] = useState([]);
    const market = "MX";

    const fetchData = async () => {
        const response = await fetch(`https://localhost:7215/api/duplicatePlaylistItems?playlistId=${playlistId}&market=${market}`,
            {
                credentials: 'include'
            });
        const jsonResponse = await response.json();
        return jsonResponse;
    }

    const loadData = async () => {
        const response = await fetchData();
        if (response) {
            setTracks(response.data);
        }
    }

    useEffect(() => {
        loadData();
    }, []);

    return (
        <>
            <p>{ playlistId }</p>
            <p>{ playlistName }</p>
            {
                tracks.map((track: DuplicateTrack, index) => (
                    <div key={index}>
                        <TrackGroupWidget group={track} />
                    </div>
                ))
            }
        </>
    );
};

export default DuplicateTracksPage;