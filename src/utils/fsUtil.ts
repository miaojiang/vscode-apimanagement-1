/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

import * as crypto from "crypto";
import * as fse from 'fs-extra';
import * as path from 'path';
import { sessionFolderKey } from "../constants";
import { ext } from '../extensionVariables';

export async function createTemporaryFile(fileName: string): Promise<string> {
    const defaultWorkspacePath = getDefaultWorkspacePath();
    const workspaceExists = await fse.pathExists(defaultWorkspacePath);
    if (!workspaceExists) {
        await setupDefaultWorkspace(defaultWorkspacePath);
    }

    const sessionFolder = getSessionWorkingFolderName();
    const filePath: string = path.join(defaultWorkspacePath, sessionFolder, fileName);
    await fse.ensureFile(filePath);
    return filePath;
}

async function setupDefaultWorkspace(defaultWorkspacePath: string) : Promise<void> {
    await fse.ensureDir(defaultWorkspacePath);
    await fse.copy(ext.context.asAbsolutePath(path.join('resources', 'backupProject')), defaultWorkspacePath, { overwrite: true, recursive: false });
    await fse.rename(path.join(defaultWorkspacePath, 'vscode-azureapim.csproj.tmp'), path.join(defaultWorkspacePath, 'vscode-azureapim.csproj'));
}

export function getDefaultWorkspacePath(): string {
    return path.join(ext.context.globalStoragePath, 'vscode-azureapim');
}

export function getSessionWorkingFolderName() : string {
    let sessionFolderName = ext.context.globalState.get(sessionFolderKey);
    if (!sessionFolderName) {
        const randomFolderNameLength: number = 12;
        const buffer: Buffer = crypto.randomBytes(Math.ceil(randomFolderNameLength / 2));
        sessionFolderName = buffer.toString('hex').slice(0, randomFolderNameLength);
        ext.outputChannel.appendLine(`Session working folder:${sessionFolderName}`);
        ext.context.globalState.update(sessionFolderKey, sessionFolderName);
    }

    return <string>sessionFolderName;
}
