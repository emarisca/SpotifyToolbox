import { Box, Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import DuplicateTrack from '../../models/duplicateTrack';
import Track from "../../models/track";

interface TrackGroupWidgetParams {
    group: DuplicateTrack
}
const TrackGroupWidget = (params: TrackGroupWidgetParams) => {
    return (
        <Box
            width="100%">
            <h1>{params.group.duplicateName}</h1>

            <Box sx={{ margin: 1 }}>
                <h3>Tracks</h3>
                <TableContainer>
                    <Table size="small">
                        <TableHead>
                            <TableRow>
                                <TableCell>Date added</TableCell>
                                <TableCell>ID</TableCell>
                                <TableCell>Name</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {params.group.tracks.map((item, index) => (
                                <TableRow key={item.track.id + index.toString()}>
                                    <TableCell>{item.addedAt}</TableCell>
                                    <TableCell>{item.track.id}</TableCell>
                                    <TableCell>{item.track.name}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>
        </Box>
    )
};

export default TrackGroupWidget;