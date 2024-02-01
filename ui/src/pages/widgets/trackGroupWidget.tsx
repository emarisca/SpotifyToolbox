import { Box, Button } from "@mui/material";
import DuplicateTrack from '../../models/duplicateTrack';
import Track from "../../models/track";

interface TrackGroupWidgetParams {
    group: DuplicateTrack
}
const TrackGroupWidget = (params: TrackGroupWidgetParams) => {
    return (
        <Box
            display="flex" alignItems="center" justifyContent="space-between">
                <h1>{ params.group.duplicateName }</h1>
            </Box>
    )
};

export default TrackGroupWidget;