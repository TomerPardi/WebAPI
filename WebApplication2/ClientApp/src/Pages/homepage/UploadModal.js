import React from "react";
import { useEffect, useState } from "react";
import { Button, Modal, Form } from "react-bootstrap";

export default function UploadModal(props) {
  const [selectedFile, setSelectedFile] = useState();
  const [preview, setPreview] = useState();
  const [disabled, setDisabled] = useState(true);
  const [show, setShow] = useState(props.show);

  const click = () => {
    props.handler(preview);
    handleClose();
  };

  const handleClose = () => {
    setShow(false);
  };
  // create a preview as a side effect, whenever selected file is changed
  useEffect(() => {
    if (!selectedFile) {
      setPreview(undefined);
      setDisabled(true);
      return;
    }

    const objectUrl = URL.createObjectURL(selectedFile);
    setPreview(objectUrl);
    setDisabled(false);

    // free memory when ever this component is unmounted
    // return () => URL.revokeObjectURL(objectUrl)
  }, [selectedFile]);

  const onSelectFile = (e) => {
    if (!e.target.files || e.target.files.length === 0) {
      setSelectedFile(undefined);
      return;
    }

    // I've kept this example simple by using the first image instead of multiple
    setSelectedFile(e.target.files[0]);
  };

  return (
    <>
      {/* <div>
                <input type='file' onChange={onSelectFile} />
                {selectedFile && <img src={preview} />}
            </div> */}
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Add file</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group
              className='mb-3 position-relative d-grid gap-3'
              controlId='newContact'
            >
              <Form.Label>Add file</Form.Label>
              <Form.Control
                id='input'
                type='file'
                accept={props.formats}
                placeholder='Upload'
                onChange={onSelectFile}
                autoFocus
              />
              <div>
                {selectedFile &&
                  (props.formats == "image/*" ? (
                    <img src={preview} className='card-img' alt='img' />
                  ) : (
                    <video className=' card-img' src={preview} controls />
                  ))}
              </div>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant='secondary' onClick={handleClose}>
            Cancel
          </Button>
          <Button
            id='sendButt'
            variant='primary'
            disabled={disabled}
            onClick={click}
          >
            Send
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
