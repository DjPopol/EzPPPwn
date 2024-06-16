# EzPPPwn v1.20

![FormPPPwn](https://github.com/DjPopol/EzPPPwn/assets/168917709/1ce2abe1-c760-4412-97a5-22946b121e63)

# Supported versions are:
- FW 7.00 / 7.01 / 7.02
- FW 7.50 / 7.51 / 7.55
- FW 8.00 / 8.01 / 8.03
- FW 8.50 / 8.52
- FW 9.00
- FW 9.03 / 9.04
- FW 9.50 / 9.51 / 9.60
- FW 10.00 / 10.01
- FW 10.50 / 10.70 / 10.71
- FW 11.00
    more can be added (PRs are welcome)

# Requirements
    - A computer with an Ethernet port  USB adapter also works.
    - Ethernet cable.
    - .Net 8.0.
    - Npcap.

# FormConfig (Config PPPwn) :
![FormConfig](https://github.com/DjPopol/EzPPPwn/assets/168917709/5552d2d6-4ac0-4ce3-bd58-ee4d4d381c06)

![FormConfig2](https://github.com/DjPopol/EzPPPwn/assets/168917709/03024c3c-2b2c-44ef-a0bb-6d983ea44369)

# FormPPPwn (Exploit):
![FormPPPwn](https://github.com/DjPopol/EzPPPwn/assets/168917709/1ce2abe1-c760-4412-97a5-22946b121e63)
- Add Update Button.
- Restyle Form.

# FormUpdate
![EzPPPwnUpdate](https://github.com/DjPopol/EzPPPwn/assets/168917709/518d1f4a-fba4-401b-b269-52619afe6c52)

![EzPPPwnUpdate2](https://github.com/DjPopol/EzPPPwn/assets/168917709/5dddc24d-a226-482d-a784-fd44ab05ae1d)

![EzPPPwnUpdate3](https://github.com/DjPopol/EzPPPwn/assets/168917709/37a11593-4ff8-43f3-9caa-6f14623ec999)

# If you don't have requirements it could help you to install it (Npcap, PPPwn C++, stage1.bin (Embedded resource)):
![image](https://github.com/DjPopol/EzPPPwn/assets/168917709/d9963422-012b-47cd-8765-2663bd0c5568)

- If you don't accept installation, Application will close.

# FormRequired (Install requirements) :
![image](https://github.com/DjPopol/EzPPPwn/assets/168917709/871661dd-8b52-429d-ac46-70ac623f3d0d)
- If you canceled installation, Application will close.


# Usage
On your PS4:
- Go to `Settings` and then `Network`
- Select `Set Up Internet connection` and choose `Use a LAN Cable`
- Choose `Custom` setup and choose `PPPoE` for `IP Address Settings`
- Enter anything for `PPPoE User ID` and `PPPoE Password`
- Choose `Automatic` for `DNS Settings` and `MTU Settings`
- Choose `Do Not Use` for `Proxy Server`
- Click `Test Internet Connection` to communicate with your computer

If the exploit fails or the PS4 crashes, you can skip the internet setup and simply click on `Test Internet Connection`. If the `pppwn.exe` is stuck waiting for a request/response, cancel it and run it again on your computer, and then click on `Test Internet Connection` on your PS4.

If the exploit works, you should see an output similar to below, and you should see `Cannot connect to network.` followed by `PPPwned` printed on your PS4.

### Example run
![image](https://github.com/DjPopol/EzPPPwn/assets/168917709/4de7c32e-f491-4f65-a5cb-be26c0a8dc33)



